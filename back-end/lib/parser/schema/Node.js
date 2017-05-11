module.exports = class Node {
  constructor (content, type = 'text') {
    this.content = content;
    this.type = type;
  }
};
