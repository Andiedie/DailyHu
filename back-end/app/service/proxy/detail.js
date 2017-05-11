const config = require('./config');
const assert = require('assert');
const _ = require('lodash');
const url = require('url');
const extract = require('./extract');

const getDetail = async function (src) {
  const hostname = url.parse(src).hostname;
  const name = hostname.match(/www\.(.+)\.com/)[1];

  assert(name, 'invalid url.');

  const siteConfig = config[name];
  assert(siteConfig, 'no such site registered.');

  return extract(src, siteConfig.articleSelector);
};

module.exports = getDetail;
