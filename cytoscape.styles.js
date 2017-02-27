'use strict'

export const cytoscapeStyles = [
  {
    selector: 'node',
    style: {
      'content': 'data(id)',
      'text-opacity': 0.5,
      'text-valign': 'center',
      'text-halign': 'right',
      'label': 'data(label)',
      'background-color': '#11479e',
    }
  },
  {
    selector: '.red-node',
    style: {
      'background-color': 'red'
    }
  },
  {
    selector: 'edge',
    style: {
      'width': 6,
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
]
