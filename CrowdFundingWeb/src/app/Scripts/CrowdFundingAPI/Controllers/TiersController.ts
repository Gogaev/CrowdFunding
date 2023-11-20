//     This code was generated by a Reinforced.Typings tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import { ICreateTierCommand } from '../../Domain/Features/TierFeature/Commands/ICreateTierCommand';
import { IUpdateTierCommand } from '../../Domain/Features/TierFeature/Commands/IUpdateTierCommand';

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
@Injectable({ providedIn: 'root' })
 export class TiersApiService
{
	constructor (private http: HttpClient) { } 
	create(command: ICreateTierCommand) : Observable<void>
	{
		const options = {
			body: command,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('POST',`${environment.baseUrl}/api/Tiers/`, options)
	}
	getAll() : Observable<void>
	{
		const options = {
			headers: { accept: 'application/json' }};
		return this.http.request<any>('GET',`${environment.baseUrl}/api/Tiers/`, options)
	}
	getById(id: string) : Observable<void>
	{
		const options = {
			params: { id: encodeURIComponent(id) },
			headers: { accept: 'application/json' }};
		return this.http.request<any>('GET',`${environment.baseUrl}/api/Tiers/${id}`, options)
	}
	delete(id: string) : Observable<void>
	{
		const options = {
			body: null,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('DELETE',`${environment.baseUrl}/api/Tiers/${id}`, options)
	}
	update(id: string) : Observable<void>
	{
		const options = {
			body: null,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('PUT',`${environment.baseUrl}/api/Tiers/${id}`, options)
	}
}
