const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');

module.exports = {
  listType: 'html',
  detailType: 'html',
  hostname: 'www.cnodejs.org',
  articleSelector: '#content > .panel:first-child',
  listItemSelector: '#topic_list .cell',
  maximumPage: 16,
  listUrl (pageNum) {
    return `https://www.cnodejs.org/?tab=good&page=${pageNum}`;
  },
  processListItem (item) {
    const $item = $(item);
    const res = {};

    res.thumbnail = $item.find('.user_avatar > img').attr('src');
    res.title = $item.find('.topic_title').text();

    const src = $item.find('.topic_title').attr('href');
    res.url = `${globalConfig.hostname}/detail?url=${url.resolve(`http://${this.hostname}`, src)}`;

    res.date = $item.find('.last_active_time').text();

    return trimAll(res);
  },
  processArticle ($) {
    const topicWrapper = $('.topic_full_title');
    topicWrapper.children('span').remove();
    const topic = topicWrapper.text();

    // 标题统一用h2包裹
    topicWrapper.replaceWith(`<h2>${topic}</h2>`);

    return $;
  }
};
