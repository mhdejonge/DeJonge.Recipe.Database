import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { CheckType } from 'src/app/check-type';

type PathVariables = string | string[];
type QueryParameters = { [name: string]: string | string[] };

@Injectable({
  providedIn: 'root'
})
export class ApiClient {
  constructor(private readonly api: HttpClient) { }

  private readonly baseUri = this.trimTrailingSlash(environment.api);

  public get<T>(route: string, path?: PathVariables | QueryParameters, params?: QueryParameters): Observable<T> {
    return this.api.get<T>(this.buildUri(route, path), { params });
  }

  public post<T>(route: string, data: any, path?: PathVariables | QueryParameters, params?: QueryParameters): Observable<T> {
    return this.api.post<T>(this.buildUri(route, path), data, { params });
  }

  public put<T>(route: string, data: any, path?: PathVariables | QueryParameters, params?: QueryParameters): Observable<T> {
    return this.api.put<T>(this.buildUri(route, path), data, { params });
  }

  public delete<T>(route: string, path?: PathVariables | QueryParameters, params?: QueryParameters): Observable<T> {
    return this.api.delete<T>(this.buildUri(route, path), { params });
  }

  public patch<T>(route: string, data?: any, path?: PathVariables | QueryParameters, params?: QueryParameters): Observable<T> {
    return this.api.patch<T>(this.buildUri(route, path), data, { params });
  }

  private buildUri(route: string, path: PathVariables | unknown) {
    route = this.trimTrailingSlash(route);
    let pathVariables = '';
    if (CheckType.string(path)) {
      pathVariables = path;
    } else if (Array.isArray(path)) {
      pathVariables = path.join('/');
    }
    return `${this.api}/${route}/${pathVariables}`;
  }

  private trimTrailingSlash(value: string): string {
    return value.replace(/\/+$/, '');
  }
}
