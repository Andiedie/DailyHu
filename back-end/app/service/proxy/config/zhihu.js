const $ = require('cheerio');
const globalConfig = require('../../../../config');
const url = require('url');
const trimAll = require('../trimAll');
const moment = require('moment');

module.exports = {
  type: 'json',
  hostname: 'news-at.zhihu.com',
  articleSelector: '',
  maximumPage: 1000,
  listUrl (pageNum) {
    return `http://news-at.zhihu.com/api/4/news/before/${moment().add(2 - pageNum, 'day').format('YYYYMMDD')}`;
  },
  processList (data) {
    data.stories.forEach(obj => {
      obj.date = data.stories.date;
    });
    return data.stories;
  },
  processListItem (item) {
    const res = {};

    if (item.images) {
      res.thumbnail = (item.images instanceof Array && item.images[0]) || item.images;
    }
    res.title = item.title;
    res.url = `${globalConfig.hostname}/detail?url=${url.resolve('http://news-at.zhihu.com', `/api/4/news/${item.id}`)}`;
    res.date = moment(item.data).format('YYYY-MM-DD');

    return trimAll(res);
  },
  extractHtml (data) {
    return data.body;
  },
  processArticle ($) {
    $('head').append(`<title>${$('h2:first-child').text()}</title>`);

    return $;
  }
};
