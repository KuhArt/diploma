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
import FileSaver from 'file-saver'
import cytoscape from 'cytoscape'


$(() => {
const submitButton = document.getElementById('submit-button')
const downloadButton = document.getElementById('download-button')
const errorMessage = document.getElementById('error-message')
const input = document.getElementById('regex-input')
const cy = window.cy = cytoscape({
  container: document.getElementById('cy'),
  boxSelectionEnabled: false
})
submitButton.addEventListener('click', () => {
  errorMessage.textContent = ''
  const polishTokens = input.value.split(' ')
  console.log(polishTokens);
  try {
    const [tree, elements] = polishToTree(polishTokens)

    elements.nodes = elements.nodes.map((node, index) => {
      node.data.label = index
      return node
    })

    elements.nodes[0].classes = 'red-node'
    elements.nodes[elements.nodes.length - 1].classes = 'red-node'

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

    window.cy = cytoscape(cytoscapeConfig)
  } catch (err) {
    errorMessage.textContent = err.message
  }
 })
const b64toBlob = (b64Data, contentType='', sliceSize=512) => {
   const byteCharacters = atob(b64Data);
   const byteArrays = [];

   for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
     const slice = byteCharacters.slice(offset, offset + sliceSize);

     const byteNumbers = new Array(slice.length);
     for (let i = 0; i < slice.length; i++) {
       byteNumbers[i] = slice.charCodeAt(i);
     }

     const byteArray = new Uint8Array(byteNumbers);

     byteArrays.push(byteArray);
   }

   const blob = new Blob(byteArrays, {type: contentType});
   return blob;
 }

 downloadButton.addEventListener('click', () => {
   console.log('click');

   const b64key = 'base64,';
   const content =  window.cy.png()
   const b64 = content.substring(content.indexOf(b64key) + b64key.length );
   const imgBlob = b64toBlob( b64, 'image/png' );

   FileSaver.saveAs( imgBlob, 'graph.png' );
 })

});
