using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace TALXIS.TestKit.Bindings.Configuration
{
    public class RoleAssignmentService
    {
        private readonly ServiceClient _serviceClient;

        public RoleAssignmentService(ServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public void UpdateSecurityRoles(string user, List<Guid> roleIds)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            Guid? userIdNullable = GetUserIdByEmailOrUsername(user);

            ArgumentNullException.ThrowIfNull(userIdNullable, nameof(userIdNullable));

            Guid userId = userIdNullable.Value;

            RemoveAllRolesFromUser(userId);

            AssignRoles(userId, roleIds);
        }

        public void AssignRoles(Guid userId, List<Guid> roleIds)
        {
            ArgumentNullException.ThrowIfNull(userId, nameof(userId));
            ArgumentNullException.ThrowIfNull(roleIds, nameof(roleIds));

            var entityReferenceCollection = new EntityReferenceCollection();

            foreach (var roleId in roleIds)
            {
                entityReferenceCollection.Add(new EntityReference("role", roleId));
            }

            _serviceClient.Associate(
                "systemuser", userId,
                new Relationship("systemuserroles_association"),
                entityReferenceCollection
                );
        }

        public void RemoveAllRolesFromUser(Guid userId)
        {
            var query = new QueryExpression("role")
            {
                ColumnSet = new ColumnSet("roleid"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = "role",
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = "systemuserroles",
                        LinkToAttributeName = "roleid",
                        JoinOperator = JoinOperator.Inner,
                        LinkCriteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("systemuserid", ConditionOperator.Equal, userId)
                            }
                        }
                    }
                }
            };

            var result = _serviceClient.RetrieveMultiple(query);

            if (result.Entities.Count == 0)
            {
                return;
            }

            var rolesToRemove = new EntityReferenceCollection();

            foreach (var entity in result.Entities)
            {
                rolesToRemove.Add(new EntityReference("role", entity.Id));
            }

            _serviceClient.Disassociate(
                "systemuser",
                userId,
                new Relationship("systemuserroles_association"),
                rolesToRemove
            );
        }

        private Guid? GetUserIdByEmailOrUsername(string user)
        {
            var query = new QueryExpression("systemuser")
            {
                ColumnSet = new ColumnSet("systemuserid", "domainname"),
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Filters =
                    {
                        new FilterExpression(LogicalOperator.Or)
                        {
                            Conditions =
                            {
                                new ConditionExpression("domainname", ConditionOperator.Equal, user),
                                new ConditionExpression("internalemailaddress", ConditionOperator.Equal, user)
                            }
                        }
                    },
                    Conditions =
                    {
                        new ConditionExpression("isdisabled", ConditionOperator.Equal, false)
                    }
                }
            };

            var result = _serviceClient.RetrieveMultiple(query);

            if (result.Entities.Count == 0)
            {
                return null;
            }

            return result.Entities[0].Id;
        }
    }
}