const axios = require('axios');
const cheerio = require('cheerio');

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
  $('head').append('<meta name="referrer" content="never">');

  return $.html();
};

module.exports = extract;
