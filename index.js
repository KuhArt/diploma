'use strict'

const ExprNameArray = require('./expr.name.array')
const {polishToInfix} = require('./polish.to.infix')

console.log('Prompt 1: Enter the \'shortName\' for Polish Expression: ')

let shortName = ''

if (shortName.length > 7 || !shortName) {
       shortName = 'Student'
}

console.log("\nPrompt 2: Enter the 'name' for Polish Expression : ")

let name = ''

if (name.length <= 7 || !name) {
  name = 'Student\'s diagram'
}

let listPostfix = []

let tokens = '"" a Join a Star Concat'

console.log("\nEnter a list of  tokens delimited Space for Polish Expression.");
console.log("For example, Polish Expression for the identifiers is: ");
console.log("\na a 9 \"\" Join Join Star Concat");
console.log("\n\"\" is the token of the empty string. ");
console.log("a is the token of the class [_a-zA-Z]");
console.log("9 is the token of the class [0-9]");
console.log("Join, Concat, Star are tokens for |, *, either {} or ^");
console.log("\nSo Regular Expression for the identifiers can be like as:");
console.log("\na*{a|9|\"\"}\nor\na*{9|a}")
console.log(polishToInfix)
let arrPolish = tokens.split(' ').filter((token) => token)
console.log('Result: ', polishToInfix(arrPolish))
