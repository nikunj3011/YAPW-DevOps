/*
    Soak testing is used to validate reliability of the system over a long time

    Run a soak test to:
    - Verify that your system doesn't suffer from bugs or memory leaks, which result in a crash or restart after
    - Verify that expected application restarts don't lose requests
    - Find bugs related to race - conditions that appear sporadically
    - Make sure your database doesn't exhaust the allotted storage space and stops
    - Make sure your logs don't exhaust the allotted disk storage
    - Make sure the external services you depend on don't stop working after a certain amount of requests are ex

    How to run a soak test:
    - Determe the maximum amount of users your system can handle
    - Get the 75 - 80 % of that value
    - Set VUS to that value
    - Run the test in 3 stages.Rump up to the VUS, stay there for 4 - 12 hours, rump down to
*/

import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '2m', target: 400 }, // ramp to 400 users load
        { duration: '3h', target: 400 }, // stay at 400 users
        { duration: '2s', target: 0 },// scale down.
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