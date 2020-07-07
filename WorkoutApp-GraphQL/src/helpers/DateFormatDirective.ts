import { SchemaDirectiveVisitor } from 'apollo-server';
import { defaultFieldResolver, GraphQLField } from 'graphql';
import { format as fnsFormat, utcToZonedTime } from 'date-fns-tz';

export class DateFormatDirective extends SchemaDirectiveVisitor {
  public visitFieldDefinition(field: GraphQLField<any, any>): void {
    const { resolve = defaultFieldResolver } = field;
    const { defaultFormat, defaultTimeZone } = this.args;

    field.resolve = async (source, { format, timeZone, ...otherArgs }, context, info) => {
      const result = await resolve.call(this, source, otherArgs, context, info);

      if (!result) {
        return null;
      }

      const date = new Date(result);

      if (format === 'timestamp') {
        return `${date.getTime()}`;
      }

      const tzDate = utcToZonedTime(date, timeZone || defaultTimeZone || '00:00');
      return fnsFormat(tzDate, format || defaultFormat || "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", {
        timeZone: timeZone || defaultTimeZone || '00:00'
      });
    };
  }
}
