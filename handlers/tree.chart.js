'use strict'

let id = 0

class TreeChart {
  constructor (element, subTrees = []) {
    this.id = `${++id}`
    this.element = element
    this.subTrees = [].concat(subTrees)
  }

  hasSubTrees () {
    return this.subTrees.length
  }

  get label () {
    return this.hasSubTrees() ? '' : this.element
  }
}

module.exports = TreeChart
