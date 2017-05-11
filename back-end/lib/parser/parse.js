const cheerio = require('cheerio');
const _ = require('lodash');
const component = require('./componentParser');
const schema = require('./schema/index');
const assert = require('assert');

const parse = function (html, selector, tagParser = component) {
  const $ = cheerio.load(html);
  const body = $(selector.body);
  let title, date;

  if (selector.title) {
    title = selector.title instanceof Function ? selector.title($) : $(selector.title).text();
  }
  if (selector.date) {
    date = selector.date instanceof Function ? selector.date($) : $(selector.date).text();
  }

  const result = new schema.Article({title, date});

  body.children().each((i, elem) => {
    const tag = elem.name.toLowerCase();
    if (tagParser[tag]) {
      const para = tagParser[tag](elem);
      assert(para instanceof schema.Paragraph, 'must be a Paragraph instance');
      result.add(para);
    }
  });

  return result;
};

module.exports = parse;
