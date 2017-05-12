const assert = require('assert');
const config = require('./config');
const axios = require('axios');

const getMetadata = async function (name) {
  const siteConfig = config[name];
  assert(siteConfig, 'no such site registered.');

  const res = {
    page: 0
  };

  const getter = siteConfig.maximumPage;
  res.page = getter instanceof Function ? await getter() : getter;

  return res;
};

module.exports = getMetadata;
