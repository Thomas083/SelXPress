<template>
    <div class="description-container">
        <h1>Description</h1>
        <p>{{ description }}</p>
    </div>
</template>

<script>
export default {
    name: 'DescriptionDetails',
    props: {
        description: {
            type: String,
            required: true 
        },
    },
}
</script>

<style scoped>

.description-container {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 0.5rem;
    width: 70%;
    word-wrap: break-word;
    text-align: justify;
}

</style>