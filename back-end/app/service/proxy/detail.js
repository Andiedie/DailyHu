const axios = require('axios');
const cheerio = require('cheerio');
const config = require('./config');

/**
 * @param url
 * @return html of the extrated website
 */
const extract = async (url, selector) => {
  const html = (await axios.get(url)).data;
  const $ = cheerio.load(html);

  const main = $(selector);
  $('body > *').remove();
  $('body').append(main);

  $('script, link').remove();
  $('head').append('<link rel="stylesheet" href="http://markdowncss.github.io/modest/css/modest.css"/>');

  return $.html();
};

module.exports = extract;
