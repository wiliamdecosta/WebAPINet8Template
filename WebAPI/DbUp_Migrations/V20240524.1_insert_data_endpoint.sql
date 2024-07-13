INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/user/list', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/user/all', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/user/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/user/create', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/user/update/{id}', 'PUT', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/user/delete', 'POST', now(), now());

INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/role/list', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/role/all', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/role/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/role/create', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/role/update/{id}', 'PUT', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/role/delete', 'POST', now(), now());

INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userole/list', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userole/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userole/list-by-user-id/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userole/list-by-role-id/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userole/create', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userrole/update/{id}', 'PUT', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/userrole/delete', 'POST', now(), now());

INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpoint/list', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpoint/all', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpoint/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpoint/create', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpoint/update/{id}', 'PUT', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpoint/delete', 'POST', now(), now());

INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/list', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/list-by-role-id/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/list-by-endpoint-id/{id}', 'GET', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/create', 'POST', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/update/{id}', 'PUT', now(), now());
INSERT INTO endpoints(path_route, method, created_date, updated_date) VALUES('/api/v1/endpointrole/delete', 'POST', now(), now());

