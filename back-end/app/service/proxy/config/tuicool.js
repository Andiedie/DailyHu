const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');

module.exports = {
  type: 'html',
  hostname: 'www.tuicool.com',
  articleSelector: '.article_detail_bg > h1, .article_body',
  listItemSelector: '.list_article_item',
  maximumPage: 20,
  listUrl (pageNum) {
    return `http://www.tuicool.com/ah/0/${pageNum}`;
  },
  processListItem (item) {
    const $item = $(item);
    const res = {};

    res.thumbnail = $item.find('img').attr('src');
    res.title = $item.find('.title a').text();

    const src = $item.find('a').attr('href');
    res.url = `${globalConfig.hostname}/detail?url=${url.resolve(`http://${this.hostname}`, src)}`;

    res.date = $($item.find('.tip span').get(2)).text();

    return trimAll(res);
  },
  processArticle ($) {
    $('h1').each((index, h1) => {
      const $h1 = $(h1);
      $h1.replaceWith(`<h2>${$h1.text()}</h2>`);
    });

    return $;
  }
};
