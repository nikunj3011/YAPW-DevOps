/*
    Load Testing is primarily concerned with assessing the current performance of your system
    in terms of concurrent users or requests per second.
    When you want to understand if your system is meeting the performance goals, this is the type of test you'll run.

    Run a load test to:
    - Assess the current performance of your system under typical and peak load
    - Make sure you are continuously meeting the performance standards as you make changes to your system

    Can be used to simulate a normal day in your business
*/

import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '5m', target: 100 }, // below normal load
        { duration: '10m', target: 100 },
        { duration: '5m', target: 0 }, // normal load
    ],
    thresholds: {
        http_req_duration: ['p(99)<150'], //99% of requests must complete below 150ms
    }
};

const API_BASE_URL = 'https://localhost:5001';

export default () => {
    http.batch([
        ['GET', `${API_BASE_URL}/Brands/all/minimal`],
        ['GET', `${API_BASE_URL}/Categories/all/minimal`],
    ]);
    sleep(1);
};
