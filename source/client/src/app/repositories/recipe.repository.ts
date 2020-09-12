import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recipe } from 'src/app/entites';
import { ApiClient } from './api.client';

@Injectable({
  providedIn: 'root'
})
export class RecipeRepository {
  constructor(private readonly api: ApiClient) { }

  private readonly route = '/api/recipe';

  getAll(): Observable<Recipe[]> {
    return this.api.get<Recipe[]>(this.route);
  }

  get(id: string): Observable<Recipe> {
    return this.api.get<Recipe>(this.route, id);
  }

  search(term: string): Observable<Recipe[]> {
    return this.api.get<Recipe[]>(this.route, { term });
  }

  insert(recipe: Recipe): Observable<string> {
    return this.api.post<string>(this.route, recipe);
  }

  update(recipe: Recipe): Observable<void> {
    return this.api.put<void>(this.route, recipe);
  }

  delete(recipe: Recipe): Observable<void> {
    return this.api.put<void>(this.route, recipe);
  }
}
