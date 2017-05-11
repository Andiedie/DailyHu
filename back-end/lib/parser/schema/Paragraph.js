const assert = require('assert');
const Node = require('./Node');

module.exports = class Paragraph {
  constructor (type = 'p') {
    this.nodes = [];
    this.type = type;
  }

  add (node) {
    assert(node instanceof Node, 'must be a Node instance.');
    this.nodes.push(node);
  }
};
