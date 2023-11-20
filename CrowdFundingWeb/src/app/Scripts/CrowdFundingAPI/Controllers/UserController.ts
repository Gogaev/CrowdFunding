//     This code was generated by a Reinforced.Typings tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import { ILoginUserCommand } from '../../Domain/Features/UserFeatures/Commands/ILoginUserCommand';
import { IRegisterUserCommand } from '../../Domain/Features/UserFeatures/Commands/IRegisterUserCommand';

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
@Injectable({ providedIn: 'root' })
 export class UserApiService
{
	constructor (private http: HttpClient) { } 
	getAll() : Observable<void>
	{
		const options = {
			headers: { accept: 'application/json' }};
		return this.http.request<any>('GET',`${environment.baseUrl}/api/User/get-users`, options)
	}
	getById(id: string) : Observable<void>
	{
		const options = {
			params: { id: encodeURIComponent(id) },
			headers: { accept: 'application/json' }};
		return this.http.request<any>('GET',`${environment.baseUrl}/api/User/${id}`, options)
	}
	login(command: ILoginUserCommand) : Observable<void>
	{
		const options = {
			body: command,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('POST',`${environment.baseUrl}/api/User/login`, options)
	}
	register(command: IRegisterUserCommand) : Observable<void>
	{
		const options = {
			body: command,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('POST',`${environment.baseUrl}/api/User/register`, options)
	}
	delete(id: string) : Observable<void>
	{
		const options = {
			body: null,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('DELETE',`${environment.baseUrl}/api/User/${id}`, options)
	}
}
