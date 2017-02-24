'use strict'

import ExprNameArray from './expr.name.array'
import {polishToInfix} from './polish.to.infix'
import {polishToTree} from './polish.to.tree'
import $ from 'jquery'
import cytoscape from 'cytoscape'

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

let tokens = "a a 9 \"\" Join Join Star Concat"

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
console.dir(polishToTree(arrPolish))


$(function(){
  const cy = window.cy = cytoscape({
    container: document.getElementById('cy'),
    boxSelectionEnabled: false,
    autounselectify: true,
    layout: {
      name: 'grid',
      cols: 5
    },
    style: [
      {
        selector: 'node',
        style: {
          'content': 'data(id)',
          'text-opacity': 0.5,
          'text-valign': 'center',
          'text-halign': 'right',
          'background-color': '#11479e',
        }
      },
      {
        selector: '#0',
        style: {
          'background-color': 'red'
        }
      },
      {
        selector: '#4',
        style: {
          'background-color': 'red'
        }
      },
      {
        selector: 'edge',
        style: {
          'width': 4,

          'line-color': '#9dbaea',
          'target-arrow-color': '#9dbaea',
          'curve-style': 'bezier',
          'label': 'data(label)',
          'target-arrow-shape': 'triangle'
        }
      },
      {
        selector: '.autorotate',
        style: {
          'edge-text-rotation': 'autorotate'
        }
      }
    ],
    elements: {
      nodes: [
        { data: { id: '0', label: 'a' } },
        { data: { id: '1' } },
        { data: { id: '2' } },
        { data: { id: '3' } },
        { data: { id: '4' } }
      ],
      edges: [
        { data: { source: '0', target: '1', label: 'a'}, classes: 'autorotate' },
        { data: { source: '1', target: '2' } },
        { data: { source: '2', target: '3' } },
        { data: { source: '2', target: '3',  label: 'a | 9' }, classes: 'autorotate'},
        { data: { source: '3', target: '4' } },
        { data: { source: '3', target: '2' } },
      ]
    },
  });
});
