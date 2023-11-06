import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {
  constructor(private tokenStorageService: TokenStorageService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.tokenStorageService.getToken();
    console.log(this.tokenStorageService.getToken());
    if (token) {
      let tokenWithoutQuotes = token.trim().slice(1, -1)
      console.log(tokenWithoutQuotes);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${tokenWithoutQuotes}`,
        },
      });
    }
    console.log('Token in interceptor:', token);
    return next.handle(request);
  }
}
