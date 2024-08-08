import typescript from '@rollup/plugin-typescript'
import {nodeResolve} from '@rollup/plugin-node-resolve'
import commonjs from '@rollup/plugin-commonjs'
import svgr from '@svgr/rollup'

import pkg from './package.json' assert {type: 'json'}


export default {
  input: 'src/index.ts',
  output: [
    {
      file: './lib/cjs/index.js',
      format: 'cjs',
    },
    {
      file: './lib/esm/index.js',
      format: 'es',
    },
  ],
  external: [
    ...Object.keys(pkg.peerDependencies || {}),
    'react/jsx-runtime',
  ],
  plugins: [
    nodeResolve(),
    commonjs(),
    typescript(),
    /* postCSS({
      plugins: [require('autoprefixer')],
    }), */
    svgr()
  ],
}