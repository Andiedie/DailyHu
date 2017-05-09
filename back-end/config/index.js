const modules = [
  './default.conf'
];
const config = {};

modules.forEach(path => {
  Object.assign(config, require(path));
});

module.exports = config;
