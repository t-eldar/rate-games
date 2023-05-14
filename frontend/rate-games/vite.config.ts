import { defineConfig } from 'vite';
import svgr from 'vite-plugin-svgr';
import react from '@vitejs/plugin-react';
import path from 'path';
import dns from 'dns';
import fs from 'fs';

dns.setDefaultResultOrder('verbatim');
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), svgr()],
  server: {
    port: 3000,
    https: {
      key: fs.readFileSync(path.resolve('localhost-key.pem')),
      cert: fs.readFileSync(path.resolve('localhost.pem')),
    },
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src/'),
    },
  },
});
