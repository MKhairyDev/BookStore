// Angular Modules
import { Injectable } from '@angular/core';
// Application Classes
import { QueryStringParameters } from '../@shared/classes/query-string-parameters';
import { UrlBuilder } from '../@shared/classes/url-builder';
import { Constants } from '../config/constants';
// Application Constants

@Injectable({
  providedIn: 'root',
})

// Returns the api endpoints urls to use in services in a consistent way
export class ApiEndpointsService {
  constructor(
    // Application Constants
    private constants: Constants
  ) {}

  /* #region EXAMPLES */
  // http://localhost:58212/api 
  public getBookByIdEndpoint = (id: string): string => this.createUrlWithPathVariables('Books', [id]); 
  public getBooksPagedEndpoint =(): string => this.createUrlWithPathVariables('Books/GetBooks'); 
  public postBookHistoryPagedEndpoint = (): string => this.createUrl('Books/PagedHistory');
  public postBooksEndpoint = (): string => this.createUrl('Books');
  public putBooksEndpoint = (id: string): string => this.createUrlWithPathVariables('Books', [id]);

  /* #endregion */

  /* #region URL CREATOR */
  // URL
  private createUrl(action: string): string {
    const urlBuilder: UrlBuilder = new UrlBuilder(this.constants.Api_Endpoint,
      action
    );
    return urlBuilder.toString();
  }

  // URL WITH QUERY PARAMS
  private createUrlWithQueryParameters(
    action: string,
    queryStringHandler?: (queryStringParameters: QueryStringParameters) => void
  ): string {
    const urlBuilder: UrlBuilder = new UrlBuilder(this.constants.Api_Endpoint, action);
    // Push extra query string params
    if (queryStringHandler) {
      queryStringHandler(urlBuilder.queryString);
    }
    return urlBuilder.toString();
  }

  // URL WITH PATH VARIABLES
  private createUrlWithPathVariables(action: string, pathVariables: any[] = []): string {
    let encodedPathVariablesUrl: string = '';
    // Push extra path variables
    for (const pathVariable of pathVariables) {
      if (pathVariable !== null) {
        encodedPathVariablesUrl += `/${encodeURIComponent(pathVariable.toString())}`;
      }
    }
    const urlBuilder: UrlBuilder = new UrlBuilder(this.constants.Api_Endpoint, `${action}${encodedPathVariablesUrl}`);
    return urlBuilder.toString();
  }
  /* #endregion */
}
