const axios = require('axios');
const parser = require('../../lib/parser');
const _ = require('lodash');

(async () => {
  // let html = (await axios.get('http://www.tuicool.com/articles/JFvymqQ')).data;
  // let html = (await axios.get('http://www.tuicool.com/articles/6bqqAjA')).data;
  let html = (await axios.get('http://www.tuicool.com/articles/Abua22A')).data;
  let result = parser.parse(html, {
    // 可以给选择器
    body: '.article_body > div',
    title: '.article_detail_bg > h1:first-child',
    // 也可以给函数处理
    date: $ => {
      const raw = $('.timestamp').text();
      return _.trim(raw.replace('时间', ''));
    }
  });
  console.log(JSON.stringify(result));
})();
