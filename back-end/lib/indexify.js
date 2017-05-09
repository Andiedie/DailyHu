const path = require('path');
const fs = require('fs');
const stack = require('callsite');

module.exports = _config => {
  const result = {};
  const config = {
    base: '.',
    exclude: [],
    // priority: [],             // loading order, first to be loaded first
    selfExclude: true,         // caller file is excluded by default
    directory: true            // whether to load modules in direct sub-directory
  };
  Object.assign(config, _config);

  const callerPath = path.resolve(stack()[1].getFileName());
  const source = path.resolve(callerPath, '..', config.base);
  const files = fs.readdirSync(source);
  files
    .map(filename => filename.replace(/\.js$/, ''))
    .filter(filename => config.directory || !fs.lstatSync(path.join(source, filename)).isDirectory())   // exclude
    .filter(filename => !(config.selfExclude && filename === path.basename(callerPath, '.js')))         // self exclude
    .filter(filename => config.exclude.indexOf(filename) === -1)                                        // directory exclude
    .forEach(filename => {
      result[filename] = require(path.join(source, filename));
    });

  return result;
};
