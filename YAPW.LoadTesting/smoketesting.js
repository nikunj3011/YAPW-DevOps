/*
Teams should run smoke tests whenever a test script is created or updated. Smoke testing should also be done whenever the relevant application code is updated.

It's a good practice to run a smoke test as a first step, with the following goals:

Verify that your test script doesn't have errors.
Verify that your system doesn't throw any errors (performance or system related) when under minimal load.
Gather baseline performance metrics of your system’s response under minimal load.
With simple logic, to serve as a synthetic test to monitor the performance and availability of production environments.
*/
import http from 'k6/http';
import { sleep } from 'k6';
export let option = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 3,
    duration: '1m'
};

export default () => {
    http.get('https://localhost:5001/Brands/all/minimal');
    sleep(1);
};