'use strict'
/*
  Polish expressions samples:

  a a 9 "" Join Join Star Concat

*/

import {polishToInfix} from './handlers/polish.to.infix'
import {polishToTree} from './handlers/polish.to.tree'
import {polishTreeToViewTree} from './handlers/polish.tree.to.view.tree'
import {cytoscapeStyles} from './cytoscape.styles'
import $ from 'jquery'
import cytoscape from 'cytoscape'


$(() => {
const submitButton = document.getElementById('submit-button')
const input = document.getElementById('regex-input')
const cy = window.cy = cytoscape()
submitButton.addEventListener('click', () => {
  const polishTokens = input.value.split(' ')
  console.log(polishTokens);
  const elements = polishTreeToViewTree(polishToTree(polishTokens))

  const cytoscapeConfig = {
     container: document.getElementById('cy'),
     boxSelectionEnabled: false,
     autounselectify: true,
     layout: {
       name: 'grid',
       cols: elements.nodes.length
     },
     style: cytoscapeStyles,
     elements
   }

   cytoscape(cytoscapeConfig)
 })
});
