import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
	duration: '1m',
  vus: 1500
};

export default function () {
	let res = http.get(
		'http://localhost/customer/get-by-email-or-document?document=string'
	);

	check(res, {
		'status is 200': (r) => r.status === 200,
	});

	sleep(0.3);
}
