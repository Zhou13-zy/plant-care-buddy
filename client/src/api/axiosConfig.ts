import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7226/api', // Update to match your backend port
});

export default api;