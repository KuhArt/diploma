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
   const [first] = subTrees

   elements.nodes.push({data: {id: `start${first.id}`, label: ++counter}})
   elements.nodes.push({data: {id:`end${first.id}`, label: ++counter}})
   elements.edges.push({data: {source: id, target: `start${first.id}`}})
   elements.edges.push({data: {source: `start${first.id}`, target: `end${first.id}`}})
   elements.edges.push({data: {source: `start${first.id}`, target: `end${first.id}`, label: first.label}})
   elements.edges.push({data: {source: `end${first.id}`, target: `start${first.id}`}})
   elements.edges.push({data: {source: `end${first.id}`, target: first.id}})
}

function handleElement (elements, {id, element, subTrees}) {
  elements.nodes.push({data: {id, label: ++counter}})

  switch (element) {
    case 'Concat':
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

  return elements
}
