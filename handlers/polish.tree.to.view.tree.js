'use strict'

let counter = 0

function handleConcat (elements, {id, subTrees}) {
  const [first, second] = subTrees
  elements.edges.push({data: {source: id, target: first.id, label: first.label}})
  elements.edges.push({data: {source: first.id, target: second.id, label: second.label}})
}

function handleJoin (elements, {id, subTrees}) {
  const [first, second] = subTrees

  elements.edges.push({data: {source: id, target: first.id, label: first.label}})
  elements.edges.push({data: {source: id, target: second.id, label: second.label}})
}

function handleStar (elements, {id, subTrees}) {
  console.log(subTrees);
  const [first] = subTrees

   elements.edges.push({data: {source: id, target: first.id}})
   elements.edges.push({data: {source: id, target: first.id, label: first.label}})
   elements.edges.push({data: {source: first.id, target: id}})
}

function handleElement (elements, {id, element, subTrees}) {
  elements.nodes.push({data: {id, label: ++counter}})

  switch (element) {
    case 'Concat':
      console.log(element);
      return handleConcat(elements, {id, element, subTrees})
    case 'Join':
      return handleJoin(elements, {id, element, subTrees})
    case 'Star':
      return handleStar(elements, {id, element, subTrees})
  }
}

function traverse (elements, treeNode) {{data: {id: 'finishNode'}}
  handleElement(elements, treeNode)
   if (treeNode.hasSubTrees) {
     return treeNode.subTrees.forEach((treeNode) => {
       traverse(elements, treeNode)
     })
   }
}

export const polishTreeToViewTree = (polishTree) => {
  let elements = {
    edges: [],
    nodes: []
  }

  traverse(elements, polishTree)

  const [lastNode] = elements.nodes.slice(-1)
  elements.nodes.push({data: {id: 'finishNode', label: ++counter}})
  elements.edges.push({data: {source: lastNode.data.id, target: 'finishNode'}})

  return elements
}
