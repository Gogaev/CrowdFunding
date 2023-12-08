import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) {}

  uploadFile(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    console.log('uploading')
    return this.http.post<any>(`${environment.baseUrl}/api/Images/upload`, formData);
  }

  getFile(id: string): Observable<any>{
    const url = `${environment.baseUrl}/api/Images/${id}`;
    return this.http.get(url, { responseType: 'blob' })
  }
}
