/*
Spike test is a variation of a stress test, but it does not gradually increase the load, instead it spikes to extreme load over a very short window of time

Run a stress test to:
- Determine how your system will perform under a sudden surge of traffic
- Determine if your system will recover once the traffic has subsided

Success is based on expectations.Systems will generally react in 1 of 4 ways
    - Excellent: system performance is not degraded during the surge of traffic.Response time is similar during low traffic and high traffic
    - Good: Response time is slower, but the system does not produce any errors.
          All requests are handled
    - Poor: System produces errors during the surge of traffic, but recovers to normal after the traffic subside
    - Bad: System crashes, and does not recover after the traffic has subsided
*/

import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '10s', target: 100 }, // below normal load
        { duration: '1m', target: 100 },
        { duration: '10s', target: 1400 }, // spike to 1400 users
        { duration: '3m', target: 1400 }, // stay at the same point
        { duration: '10s', target: 100 }, // scale down. Recovery stage.
        { duration: '3m', target: 100 },
        { duration: '10s', target: 0 },
    ],
};

const API_BASE_URL = 'https://localhost:5001';

export default () => {
    http.batch([
        ['GET', `${API_BASE_URL}/Brands/all/minimal`],
        ['GET', `${API_BASE_URL}/Categories/all/minimal`],
    ]);
    sleep(1);
};
