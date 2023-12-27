﻿using Surveys.Domain.Base;

namespace Surveys.Domain;

public class Anamnesis : Identity
{
    public Guid AnamnesisTemplateId { get; set; }
    public virtual AnamnesisTemplate AnamnesisTemplate { get; set; } = null!;

    public virtual ICollection<AnamnesisAnswer>? Answers { get; set; }

    public bool IsComplete { get; set; }

    public Guid SurveyId { get; set; }
    public virtual Survey Survey { get; set; } = null!;
}