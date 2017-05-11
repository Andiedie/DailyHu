const assert = require('assert');
const Paragraph = require('./Paragraph');

module.exports = class Article {
  constructor ({title, date, paras} = {}) {
    this.title = title;
    this.date = date;
    this.paras = paras || [];
  }

  add (para) {
    assert(para instanceof Paragraph, 'must be a Paragraph instance');
    this.paras.push(para);
  }
};
