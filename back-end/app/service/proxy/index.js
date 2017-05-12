const indexify = require('../../../lib/indexify');

module.exports = indexify({
  include: [
    ['list.js', 'getList'],
    ['meta.js', 'getMetadata'],
    ['detail', 'getDetail']
  ]
});
