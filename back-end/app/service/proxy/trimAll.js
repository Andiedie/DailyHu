const _ = require('lodash');

module.exports = function (obj) {
  const res = {};
  Object.assign(res, obj);
  for (const [key, value] of Object.entries(res)) {
    res[key] = _.trim(value);
  }
  return res;
};
