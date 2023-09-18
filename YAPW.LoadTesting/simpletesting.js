import http from 'k6/http';
import { sleep } from 'k6';
export let option = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 10,
    iterations: 100,
    duration: '100s'
};

export default () => {
    http.get('https://localhost:5001/Brands/all/minimal');
    sleep(1);
};