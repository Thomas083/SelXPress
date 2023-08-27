<template>
    <div class="tags-container">
        <div v-for="tag in tagsList">
            <div class="tag">{{tag.name}}</div>
            <div class="separation"></div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'TagsList',
    data() {
        return {
            tags: [
                {
                    id: 1,
                    name: 'Fishing',
                    categoryId: 1
                },
                {
                    id: 2,
                    name: 'Football',
                    categoryId: 10
                }
            ],
        }
    },
    computed: {
        tagsList() {
            const tagList = []
            for (let i = 0; i < this.tags.length; i++) {
                if (this.tags[i].categoryId == this.$route.params.id) tagList.push(this.tags[i])
            }
            return tagList
        }
    },
}

</script>

<style scoped>
.tags-container {
    display: flex;
    flex-direction: column;
    background-color: var(--main-white);
    gap: 0.5rem;
    border-radius: 15px;
    padding: 1rem;
    height: max-content;
    max-width: 300px;
    align-items: flex-start;
}

.tag {
    font-size: 2rem;
    cursor: pointer;
}

.tag:hover {
    opacity: 0.7;
}

.separation {
    border: 1px var(--main-grey-product) solid;
    width: 90%;
}
</style>