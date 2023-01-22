import { systemError } from "../entities";
import ErrorService from "./error.service"
import { AppError } from "../enum";

export class RequestHelper {
  public static parseNumericValue( input: string): number | systemError {
    let id: number = -1;
    if (isNaN(Number(input))) {
      // ToDO: Error handling
      return ErrorService.getErrors(AppError.NaNError);
    }

    if (input != null) {
      id = parseInt(input);
      
    } else {
      return ErrorService.getErrors(AppError.EmptyNumberError);
      // TODO: Error handling
    }
    return id;
  }
}
