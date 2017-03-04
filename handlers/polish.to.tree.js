'use strict'
const TreeChart = require('./tree.chart')
let counter = 0
let idCounter = 0

function isSimpleOperand(arg){
  return  !(arg.element == "Star" ||
          arg.element == "Concat" ||
          arg.element == "Join")
}

function joinSimpleOperands (s1, s2) {
  if (!s1) {
    return s2
  }

  if (!s2) {
    return s1
  }

  if (s1 === s2) {
    return s1
  }
  console.log(s1, s2);
  return `${s1}|${s2}`
}

function handleStar (stack, viewStack) {
  const arg1 = stack.pop()
  const viewArg1 = viewStack.pop()
  if (!arg1) {
    throw new Error('Polish notation is invalid')
  }
  let treeChart
  let viewRes
  if (arg1.element !== 'Star') {
    treeChart = new TreeChart('Star', arg1)
    stack.push(treeChart)

    const [firstId, secondId] = [++idCounter, ++idCounter]

    viewArg1.nodes.unshift({data: {id: firstId, label: ++counter}})
    viewArg1.nodes.push({data: {id: secondId, label: ++counter}})
    viewArg1.edges.push({data: {source: viewArg1.start, target: viewArg1.end}})
    viewArg1.edges.unshift({data: {source: viewArg1.end, target: viewArg1.start}})
    viewArg1.edges.unshift({data: {source: firstId, target: viewArg1.start}})
    viewArg1.edges.push({data: {source: viewArg1.end, target: secondId}})

    viewArg1.start = firstId
    viewArg1.end = secondId

    viewStack.push(viewArg1)
    return
  }

   treeChart = arg1
   viewRes = viewArg1
   viewStack.push(viewArg1)
  stack.push(treeChart)
}

function handleConcat (stack, viewStack) {
  const arg2 = stack.pop()
  const arg1 = stack.pop()
  const viewArg1 = viewStack.pop()
  const viewArg2 = viewStack.pop()
  if (!arg1 || !arg2) {
    throw new Error('Polish notation is invalid')
  }
  let viewArg = {}

  viewArg.nodes = viewArg2.nodes
  .concat(viewArg1.nodes)
  .filter((node) => {
    return node.data.id !== viewArg2.end
  })

  viewArg.edges = viewArg2.edges.concat(viewArg1.edges).map((edge) => {
    if (edge.data.target === viewArg2.end) {
      return {data: {source: edge.data.source, target: viewArg1.start, label: edge.data.label}}
    }

    return edge
  })

  viewArg.start = viewArg2.start
  viewArg.end = viewArg1.end

  viewStack.push(viewArg)
  stack.push(new TreeChart('Concat', [arg1, arg2]))
}

function handleJoin (stack, viewStack) {
  const arg2 = stack.pop()
  const arg1 = stack.pop()
  const viewArg1 = viewStack.pop()
  const viewArg2 = viewStack.pop()
  if (!arg1 || !arg2) {
    throw new Error('Polish notation is invalid')
  }
  const isSimpleOperands = [arg1, arg2].every(isSimpleOperand)
  if (!isSimpleOperands) {
    stack.push('Join', '', [arg1, arg2])
    let viewArg = {}
    const [firstId, secondId] = [++idCounter, ++idCounter]

    viewArg.nodes = [{data: {id: firstId, label: ++counter}}]
    .concat(viewArg1.nodes)
    .concat(viewArg2.nodes)
    .filter((node) => {
      return node.data.id !== viewArg1.start &&
             node.data.id !== viewArg1.end &&
             node.data.id !== viewArg2.start &&
             node.data.id !== viewArg2.end
    })
    .concat({data: {id: secondId, label: ++counter}})

    viewArg.edges = viewArg1.edges.concat(viewArg2.edges).map((edge) => {
      if (edge.data.source === viewArg1.start || edge.data.source === viewArg2.start) {
        return {data: {source: firstId, target: edge.data.target, label: edge.data.label}}
      }

      if (edge.data.target === viewArg1.end || edge.data.target === viewArg2.end) {
        return {data: {source: edge.data.source, target: secondId, label: edge.data.label}}
      }

      return edge
    })

    viewArg.start = firstId
    viewArg.end = secondId

    viewStack.push(viewArg)
    return
  }

  const label = joinSimpleOperands(arg1.element, arg2.element)
  viewStack.push(addEdge(label))
  stack.push(new TreeChart(label))

}

function addEdge (s) {
  const [firstId, secondId] = [++idCounter, ++idCounter]
  let nodes = []
  let edges = []
  nodes.push({data: {id: firstId, label: ++counter}})
  nodes.push({data: {id: secondId, label: ++counter}})
  edges.push({data: {source: firstId, target: secondId, label: s}})

  return {nodes, edges, start: firstId, end: secondId}
}

export const polishToTree = (polish) => {
  console.log(TreeChart);
  let stack = []
  let viewStack = []

  polish.forEach((s) => {
    if (s.length === 1) {
      viewStack.push(addEdge(s))
      stack.push(new TreeChart(s))
      return
    }

    if(s === '""') {
      viewStack.push(addEdge(s))
      stack.push(new TreeChart(''))
      return
    }

    switch (s) {
      case 'Star':
        return handleStar(stack, viewStack)
      case 'Concat':
        return handleConcat(stack, viewStack)
      case 'Join':
        return handleJoin(stack, viewStack)
    }

    throw new Error('Polish notation is invalid')
  })

  return [stack.pop(), viewStack.pop()]
}
