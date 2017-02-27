'use strict'
const TreeChart = require('./tree.chart')

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


function handleStar (stack) {
  const arg1 = stack.pop()
  const treeChart = arg1.element !== 'Star'
                    ? new TreeChart('Star', arg1)
                    : arg1

  stack.push(treeChart)
}

function handleConcat (stack) {
  const arg2 = stack.pop()
  const arg1 = stack.pop()

  stack.push(new TreeChart('Concat', [arg1, arg2]))
}

function handleJoin (stack) {
  const arg2 = stack.pop()
  const arg1 = stack.pop()
  const isSimpleOperands = [arg1, arg2].every(isSimpleOperand)
  if (!isSimpleOperands) {
    stack.push('Join', '', [arg1, arg2])
    return
  }
  stack.push(new TreeChart(joinSimpleOperands(arg1.element, arg2.element)))

}

export const polishToTree = (polish) => {
  console.log(TreeChart);
  let stack = []

  polish.forEach((s) => {
    if (s.length === 1) {
      stack.push(new TreeChart(s))
      return
    }

    if(s === '""') {
      stack.push(new TreeChart(''))
      return
    }

    switch (s) {
      case 'Star':
        return handleStar(stack)
      case 'Concat':
        return handleConcat(stack)
      case 'Join':
        return handleJoin(stack)
    }
  })

  return stack.pop()
}
