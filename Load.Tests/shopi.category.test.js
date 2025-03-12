import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
	duration: '2s',
	vus: 2,
};

export default function () {
  let payload = JSON.stringify({
    name: `Category ${Math.floor(Math.random() * 1000)}`,
    description: `Description for category ${Math.floor(Math.random() * 1000)}`,
  });

  let headers = {
    'Content-Type': 'application/json',
    'Authorization': `Bearer SEU_TOKEN_AQUI`,
  };

  let res = http.post('http://localhost:5200/api/v1/category/create', payload, { headers });

  check(res, { 'status is 201': (r) => r.status === 201 });
}
