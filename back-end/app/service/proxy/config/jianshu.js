const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');
const moment = require('moment');

module.exports = {
  listType: 'html',
  detailType: 'html',
  hostname: 'www.jianshu.com',
  articleSelector: '.article',
  listItemSelector: 'li[data-note-id]',
  maximumPage: 20,
  listUrl (pageNum) {
    return `http://www.jianshu.com/trending/monthly?page=${pageNum}`;
  },
  processListItem (item) {
    const $item = $(item);
    const res = {};

    res.thumbnail = $item.find('img').attr('src');
    res.thumbnail = 'http:' + res.thumbnail;
    res.title = $item.find('.title').text();

    const src = $item.find('.title').attr('href');
    res.url = `${globalConfig.hostname}/detail?url=${url.resolve(`http://${this.hostname}`, src)}`;

    res.date = moment($item.find('.time').attr('data-shared-at')).format('YYYY-MM-DD HH:mm');

    return trimAll(res);
  },
  processArticle ($) {
    $('h1').each((index, h1) => {
      const $h1 = $(h1);
      $h1.replaceWith(`<h2>${$h1.text()}</h2>`);
    });

    $('.author, .modal-wrap').remove();

    const notebook = $('a.notebook');
    notebook.attr('href', url.resolve('http://www.jianshu.com', notebook.attr('href')));
    notebook.attr('target', '_blank');

    return $;
  }
};
