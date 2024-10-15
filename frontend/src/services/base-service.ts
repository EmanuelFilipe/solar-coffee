import axios, { AxiosInstance } from 'axios';

export class BaseService {
    protected http: AxiosInstance;
    protected API_URL = 'https://localhost:5000/api';

    constructor() {
        this.http = axios.create({
          baseURL: this.API_URL
        });
    }
}