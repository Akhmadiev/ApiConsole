﻿namespace BackendTask1.Interfaces.Country
{
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using Castle.MicroKernel.Registration;
    using System.Collections.Generic;

    /// <summary>
    /// Country's interface
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Save entites
        /// </summary>
        void Save<T>(List<T> entities) where T : Entity;

        /// <summary>
        /// Save entity
        /// </summary>
        void Save<T>(T entity) where T : Entity;

        /// <summary>
        /// Parse result from json<string>
        /// </summary>
        List<T> ParsingResult<T, T2>(List<T2> data) where T : Entity where T2 : class;
    }
}
