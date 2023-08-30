<template>
    <div class="tags-container">
        <div v-for="tag in tagsList">
            <div class="tag">{{tag.name}}</div>
            <div class="separation"></div>
        </div>
    </div>
</template>

<script>
import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';

export default {
    name: 'TagsList',
    data() {
        return {
            tags: null
        }
    },
    computed: {
        tagsList() {
            if (this.tags) {
                const tagList = []
                for (let i = 0; i < this.tags.length; i++) {
                    if (this.tags[i].categoryId == this.$route.params.id) tagList.push(this.tags[i])
                };
                return tagList
            }
        }
    },
    mounted () {
        GET(ENDPOINTS.GET_ALL_TAGS)
        .then((response) => {
            this.tags = response.data
        })
        .catch((error) => {
            console.dir(error)
        });
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