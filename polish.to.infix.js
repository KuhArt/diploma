'use strict'
const {stringToConcat} = require('./string.to.concat')

function getTwoArgs (stack, separator) {
  const arg2 = stack.pop()
  const arg1 = stack.pop()

  if (!arg1) {
    return
  }

  stack.push(`(${arg1}${separator}${arg2})`)
}

export const polishToInfix = (polish) => {
  let stack = []

  polish.forEach((s) => {
    console.log(stack);
    if (s.length === 1) {
      stack.push(s)
      return
    }

    if (s === '""') {
      return stack.push('""')
    }

    if (!stack.length) {
      return
    }

    switch (s) {
      case 'Star':
        return stack.push(`{${stack.pop()}}`)
      case 'Concat':
        return getTwoArgs(stack, '*')
      case 'Join':
        return getTwoArgs(stack, '|')
     }
  })

  return stack.pop() || ''
}
