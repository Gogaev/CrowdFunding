//     This code was generated by a Reinforced.Typings tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
@Injectable({ providedIn: 'root' })
 export class ImagesApiService
{
	constructor (private http: HttpClient) { } 
	uploadPhoto(file: ) : Observable<string>
	{
		const options = {
			body: file,
			headers: { accept: 'application/json' }};
		return this.http.request<any>('POST',`${environment.baseUrl}/api/Images/upload`, options)
	}
	getPhoto(id: string) : Observable<void>
	{
		const options = {
			params: { id: encodeURIComponent(id) },
			headers: { accept: 'application/json' }};
		return this.http.request<any>('GET',`${environment.baseUrl}/api/Images/${id}`, options)
	}
}