const config = require('./default.conf');

if (process.env.NODE_ENV === 'production') {
  const prod = require('./prod.conf');
  Object.assign(config, prod);
}

module.exports = config;
