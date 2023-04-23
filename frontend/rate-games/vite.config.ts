import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from "path";
import dns from "dns";
import fs from "fs";
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    https: {
      key: fs.readFileSync(path.resolve("localhost-key.pem")),
      cert: fs.readFileSync(path.resolve("localhost.pem")),
    },
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src/'),
    },
  },
});
