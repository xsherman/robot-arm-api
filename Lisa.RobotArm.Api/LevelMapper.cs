﻿using Lisa.Common.TableStorage;
using Lisa.Common.WebApi;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Lisa.RobotArm.Api
{
    public class LevelMapper
    {
        public static ITableEntity ToEntity(dynamic model, object location)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model");
            }

            dynamic entity = new DynamicEntity();
            entity.Slug = model.Slug.ToString();
            entity.Contents = model.Contents.ToString();
            entity.Url = location;

            dynamic metadata = model.GetMetadata();
            if (metadata == null)
            {
                entity.Id = Guid.NewGuid();
                entity.RowKey = entity.Id.ToString();
                entity.PartitionKey = entity.Slug.ToString();
            }

            return entity;
        }

        public static DynamicModel ToModel(dynamic entity, bool k)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity");
            }

            dynamic model = new DynamicModel();
            if (k)
            {
                model.Url = entity.Contents;
                model.Contents = entity.Contents;
            } else {
                model.Slug = entity.Slug.ToString();
                model.Contents = entity.Contents;
                model.Url = entity.Url;
            }
            var metadata = new
            {
                PartitionKey = entity.PartitionKey,
                RowKey = entity.RowKey,
            };
            model.SetMetadata(metadata);

            return model;
        }
    }
}